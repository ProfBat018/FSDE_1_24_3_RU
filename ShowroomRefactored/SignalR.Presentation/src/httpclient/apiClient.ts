const API_URL = import.meta.env.VITE_API_URL || "http://localhost:5056";

export class ApiClient {
  private baseUrl: string;

  constructor() {
    this.baseUrl = API_URL;
  }

  async createNotification(accessToken?: string): Promise<void> {
    const headers: Record<string, string> = {
      "Content-Type": "application/json",
    };

    if (accessToken) {
      headers["Authorization"] = `Bearer ${accessToken}`;
    }

    const url = `${this.baseUrl}/api/v1/Test/Notification`;
    console.log("🚀 Sending notification request to:", url);
    console.log("📋 Headers:", headers);

    try {
      const response = await fetch(url, {
        method: "POST",
        headers,
      });

      console.log("📡 Response status:", response.status);
      console.log(
        "📡 Response headers:",
        Object.fromEntries(response.headers.entries())
      );

      if (!response.ok) {
        const errorText = await response.text();
        console.error("❌ Response error:", errorText);
        throw new Error(
          `HTTP error! status: ${response.status}, message: ${errorText}`
        );
      }

      const responseText = await response.text();
      console.log("✅ Notification created successfully");
      console.log("📄 Response body:", responseText);
    } catch (error) {
      console.error("❌ Error creating notification:", error);
      throw error;
    }
  }
}

export const apiClient = new ApiClient();
