import React, { useState, useEffect } from "react";
import { apiClient } from "../httpclient/apiClient";
import { useSignalR } from "../hooks/useSignalR";
import * as Toast from "@radix-ui/react-toast";
import { Bell, CheckCircle, Key } from "lucide-react";

export const NotificationButton: React.FC = () => {
  const [isLoading, setIsLoading] = useState(false);
  const [notifications, setNotifications] = useState<string[]>([]);
  const [toastOpen, setToastOpen] = useState(false);
  const [lastNotification, setLastNotification] = useState<string>("");
  const [accessToken, setAccessToken] = useState<string>("");
  const {
    isConnected,
    setupNotificationListener,
    connection,
    reconnectWithNewToken,
  } = useSignalR();

  useEffect(() => {
    if (isConnected) {
      setupNotificationListener((message: string) => {
        console.log("Notification received in component:", message);
        setNotifications((prev) => [...prev, message]);
        setLastNotification(message);
        setToastOpen(true);
      });
    }
  }, [isConnected, setupNotificationListener]);

  const handleCreateNotification = async () => {
    setIsLoading(true);
    try {
      // Если токен введен, сохраняем его и переподключаемся
      if (accessToken) {
        localStorage.setItem("accessToken", accessToken);
        console.log("🔑 Token saved to localStorage");
        
        // Переподключаемся с новым токеном
        await reconnectWithNewToken();
        
        // Небольшая задержка для установки соединения
        await new Promise(resolve => setTimeout(resolve, 500));
      }

      await apiClient.createNotification(accessToken);
    } catch (error) {
      console.error("Failed to create notification:", error);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-gray-50 flex items-center justify-center p-4">
      <div className="bg-white rounded-lg shadow-lg p-8 max-w-md w-full">
        <div className="text-center mb-8">
          <Bell className="w-16 h-16 mx-auto mb-4 text-blue-600" />
          <h1 className="text-2xl font-bold text-gray-900 mb-2">
            SignalR Notifications Test
          </h1>
          <div className="flex items-center justify-center gap-2">
            <div
              className={`w-3 h-3 rounded-full ${
                isConnected ? "bg-green-500" : "bg-red-500"
              }`}
            />
            <span className="text-sm text-gray-600">
              {isConnected ? "Connected" : "Disconnected"}
            </span>
          </div>
        </div>

        <div className="mb-6">
          <label
            htmlFor="accessToken"
            className="block text-sm font-medium text-gray-700 mb-2 flex items-center gap-2"
          >
            <Key className="w-4 h-4" />
            Access Token
          </label>
          <div className="relative">
            <input
              id="accessToken"
              type="text"
              value={accessToken}
              onChange={(e) => setAccessToken(e.target.value)}
              placeholder="Вставьте ваш JWT access token..."
              className="w-full px-3 py-2 pr-10 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent"
            />
            {accessToken && (
              <div className="absolute right-3 top-1/2 transform -translate-y-1/2">
                <CheckCircle className="w-4 h-4 text-green-500" />
              </div>
            )}
          </div>
          {accessToken && (
            <p className="text-xs text-green-600 mt-1 flex items-center gap-1">
              <CheckCircle className="w-3 h-3" />
              Токен будет использован для SignalR и API
            </p>
          )}
          <p className="text-xs text-gray-500 mt-1">
            💡 При нажатии кнопки произойдет переподключение SignalR с токеном
          </p>
        </div>

        <div className="space-y-3">
          <button
            onClick={handleCreateNotification}
            disabled={isLoading || !isConnected}
            className="w-full bg-blue-600 hover:bg-blue-700 disabled:bg-gray-400 disabled:cursor-not-allowed text-white font-medium py-3 px-4 rounded-lg transition-colors duration-200 flex items-center justify-center gap-2"
          >
            {isLoading ? (
              <>
                <div className="w-4 h-4 border-2 border-white border-t-transparent rounded-full animate-spin" />
                Creating...
              </>
            ) : (
              <>
                <Bell className="w-4 h-4" />
                Create Notification
              </>
            )}
          </button>

          <div className="grid grid-cols-2 gap-2">
            <button
              onClick={() => {
                console.log("🔍 Debug Info:");
                console.log("- isConnected:", isConnected);
                console.log("- connection:", connection);
                console.log("- connection state:", connection?.state);
                console.log("- connection ID:", connection?.connectionId);
                console.log("- notifications count:", notifications.length);
                console.log("- API URL:", import.meta.env.VITE_API_URL);
                console.log("- Access Token (input):", accessToken ? "Set" : "Not set");
                console.log("- Access Token (localStorage):", localStorage.getItem("accessToken") ? "Set" : "Not set");
              }}
              className="bg-gray-500 hover:bg-gray-600 text-white font-medium py-2 px-4 rounded-lg transition-colors duration-200 text-sm"
            >
              🔍 Debug
            </button>

            <button
              onClick={() => {
                console.log("🧪 Testing notification display...");
                setNotifications((prev) => [
                  ...prev,
                  `Test notification ${Date.now()}`,
                ]);
                setLastNotification(`Test notification ${Date.now()}`);
                setToastOpen(true);
              }}
              className="bg-green-500 hover:bg-green-600 text-white font-medium py-2 px-4 rounded-lg transition-colors duration-200 text-sm"
            >
              🧪 Test UI
            </button>
          </div>
        </div>

        {notifications.length > 0 && (
          <div className="mt-6">
            <h3 className="text-lg font-semibold text-gray-900 mb-3">
              Recent Notifications ({notifications.length})
            </h3>
            <div className="space-y-2 max-h-40 overflow-y-auto">
              {notifications
                .slice(-5)
                .reverse()
                .map((notification, index) => (
                  <div
                    key={index}
                    className="bg-gray-100 p-3 rounded-lg text-sm text-gray-700"
                  >
                    {notification}
                  </div>
                ))}
            </div>
          </div>
        )}

        <Toast.Provider>
          <Toast.Root
            open={toastOpen}
            onOpenChange={setToastOpen}
            className="bg-white border border-gray-200 rounded-lg shadow-lg p-4 mb-4"
          >
            <div className="flex items-center gap-3">
              <CheckCircle className="w-5 h-5 text-green-500" />
              <div>
                <Toast.Title className="font-medium text-gray-900">
                  New Notification
                </Toast.Title>
                <Toast.Description className="text-sm text-gray-600">
                  {lastNotification}
                </Toast.Description>
              </div>
            </div>
          </Toast.Root>
          <Toast.Viewport className="fixed bottom-0 right-0 flex flex-col p-6 gap-2 w-96 max-w-[100vw] m-0 list-none z-50 outline-none" />
        </Toast.Provider>
      </div>
    </div>
  );
};
