import { useEffect, useState } from 'react';
import apiService from '../services/api';

interface StatusData {
    status: string;
    timestamp: string;
    version: string;
    environment: string;
}

const TSPVisualizer = () => {
    const [apiData, setApiData] = useState<StatusData | null>(null);
    const [error, setError] = useState<string>('');

    useEffect(() => {
        const fetchStatus = async () => {
            try {
                const data = await apiService.getStatus();
                setApiData(data);
            } catch (error) {
                setError('Failed to fetch API status');
                console.error('API Error:', error);
            }
        };

        fetchStatus();
    }, []);

    if (error) {
        return <div className="text-red-600 p-4">{error}</div>;
    }

    return (
        <div className="p-4">
            <h1 className="text-2xl font-bold mb-4">TSP Route Visualization</h1>
            {apiData ? (
                <div className="border p-4 rounded">
                    <p>Status: {apiData.status}</p>
                    <p>Time: {apiData.timestamp}</p>
                    <p>Version: {apiData.version}</p>
                    <p>Environment: {apiData.environment}</p>
                </div>
            ) : (
                <p>Loading...</p>
            )}
        </div>
    );
};

export default TSPVisualizer;