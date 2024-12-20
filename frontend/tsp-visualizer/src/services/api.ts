import axios from 'axios';

export interface PointDto {
    id: string;
    x: number;
    y: number;
}

export interface ConnectionDto {
    fromPoint: PointDto;
    toPoint: PointDto;
    distance: number;
    isOptimal: boolean;
}

export interface RouteDto {
    id: number;
    points: PointDto[];
    connections: ConnectionDto[];
    totalDistance: number;
    algorithmDto: number;
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
    calculateRoute: async (input: { algorithm: number, points: PointDto[] }): Promise<RouteDto> => {
        const response = await api.post<RouteDto>('/route/calculate', input);
        return response.data;
    },
};

export const mapService = {
    generateMap: async (input: { gridSize: number, numberOfPoints: number }): Promise<PointDto[]> => {
        const response = await api.post<PointDto[]>('/map/generate', input);
        return response.data;
    }
};

export default apiService;