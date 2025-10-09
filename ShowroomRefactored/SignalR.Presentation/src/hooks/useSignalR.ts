import { useEffect, useState, useCallback, useRef } from "react";
import * as signalR from "@microsoft/signalr";

const API_URL = import.meta.env.VITE_API_URL || "http://localhost:5056";

export const useSignalR = () => {
  const [connection, setConnection] = useState<signalR.HubConnection | null>(
    null
  );
  const [isConnected, setIsConnected] = useState(false);
  const notificationCallbackRef = useRef<((message: string) => void) | null>(null);

  const playNotificationSound = () => {
    // Создаем простой звук уведомления
    const audioContext = new (window.AudioContext ||
      (window as unknown as { webkitAudioContext: typeof AudioContext })
        .webkitAudioContext)();
    const oscillator = audioContext.createOscillator();
    const gainNode = audioContext.createGain();

    oscillator.connect(gainNode);
    gainNode.connect(audioContext.destination);

    oscillator.frequency.setValueAtTime(800, audioContext.currentTime);
    oscillator.frequency.setValueAtTime(600, audioContext.currentTime + 0.1);
    oscillator.frequency.setValueAtTime(800, audioContext.currentTime + 0.2);

    gainNode.gain.setValueAtTime(0.3, audioContext.currentTime);
    gainNode.gain.exponentialRampToValueAtTime(
      0.01,
      audioContext.currentTime + 0.3
    );

    oscillator.start(audioContext.currentTime);
    oscillator.stop(audioContext.currentTime + 0.3);
  };

  const setupListenerOnConnection = (conn: signalR.HubConnection) => {
    // Удаляем предыдущий слушатель если есть
    conn.off("ReceiveNotification");

    // Устанавливаем новый слушатель
    conn.on("ReceiveNotification", (message: string) => {
      console.log("🔔 Received notification:", message);
      if (notificationCallbackRef.current) {
        notificationCallbackRef.current(message);
        playNotificationSound();
      }
    });

    console.log("✅ ReceiveNotification listener установлен на соединение");
  };

  const createConnection = () => {
    const token = localStorage.getItem("accessToken") || "";
    console.log(
      "Setting up SignalR connection to:",
      `${API_URL}/hubs/notification`,
      token ? "with token" : "without token"
    );

    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl(
        `${API_URL}/hubs/notification?access_token=${token}`
      )
      .withAutomaticReconnect()
      .build();

    return newConnection;
  };

  useEffect(() => {
    const newConnection = createConnection();

    newConnection
      .start()
      .then(() => {
        console.log("✅ SignalR Connected successfully");
        setupListenerOnConnection(newConnection); // Устанавливаем слушатель сразу после подключения
        setIsConnected(true);
        setConnection(newConnection);
      })
      .catch((error) => {
        console.error("❌ SignalR Connection Error:", error);
        setIsConnected(false);
      });

    newConnection.onclose((error) => {
      console.log("🔌 SignalR Disconnected", error ? `Error: ${error}` : "");
      setIsConnected(false);
    });

    newConnection.onreconnecting(() => {
      console.log("🔄 SignalR Reconnecting...");
    });

    newConnection.onreconnected(() => {
      console.log("✅ SignalR Reconnected");
      setupListenerOnConnection(newConnection); // Переустанавливаем слушатель после автоматического переподключения
      setIsConnected(true);
    });

    return () => {
      console.log("🧹 Cleaning up SignalR connection");
      newConnection.stop();
    };
  }, []);

  // Функция для регистрации callback из компонента
  const setupNotificationListener = useCallback((
    onNotification: (message: string) => void
  ) => {
    console.log("📝 Registering notification callback");
    notificationCallbackRef.current = onNotification;
  }, []);

  const reconnectWithNewToken = async () => {
    if (connection) {
      console.log("🔄 Reconnecting SignalR with new token...");
      await connection.stop();
    }

    // Создаем новое соединение с обновленным токеном из localStorage
    const token = localStorage.getItem("accessToken") || "";
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl(
        `${API_URL}/hubs/notification?access_token=${token}`
      )
      .withAutomaticReconnect()
      .build();

    // Настраиваем обработчики событий
    newConnection.onclose((error) => {
      console.log("🔌 SignalR Disconnected", error ? `Error: ${error}` : "");
      setIsConnected(false);
    });

    newConnection.onreconnecting(() => {
      console.log("🔄 SignalR Reconnecting...");
    });

    newConnection.onreconnected(() => {
      console.log("✅ SignalR Reconnected");
      setupListenerOnConnection(newConnection);
      setIsConnected(true);
    });

    try {
      await newConnection.start();
      console.log("✅ SignalR Reconnected with new token");
      setupListenerOnConnection(newConnection);
      setIsConnected(true);
      setConnection(newConnection);
    } catch (error) {
      console.error("❌ SignalR Reconnection Error:", error);
      setIsConnected(false);
    }
  };

  return {
    connection,
    isConnected,
    setupNotificationListener,
    reconnectWithNewToken,
  };
};
