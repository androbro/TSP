import axios from 'axios';


export interface PointDto {
    x: number;
    y: number;
}

export interface RouteDto {
    points: PointDto[];
    totalDistance: number;
    calculationTime: string;
}

// Create axios instance with default config
const api = axios.create({
    baseURL: 'https://localhost:44374/api', // Adjust this to match your .NET API port
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

export const routeService = {
    calculateRoute: async (): Promise<RouteDto> => {
        const response = await api.get<RouteDto>('/route/calculate');
        return response.data;
    },
};


export default apiService;