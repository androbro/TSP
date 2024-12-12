import axios from 'axios';

// Create axios instance with default config
const api = axios.create({
    baseURL: 'https://localhost:44374/TSP', // Adjust this to match your .NET API port
    headers: {
        'Content-Type': 'application/json',
    },
});

// Type for the status response
interface StatusResponse {
    status: string;
    timestamp: string;
    version: string;
    environment: string;
}

// API methods
export const apiService = {
    // Get API status
    getStatus: async (): Promise<StatusResponse> => {
        const response = await api.get<StatusResponse>('/Status');
        return response.data;
    },
};

export default apiService;