import { useEffect, useRef, useState } from 'react';
import axios from 'axios';

interface Point {
    id: number;
    x: number;
    y: number;
}

interface Route {
    points: Point[];
    connections: [number, number][];
}

const TSPVisualizer = () => {
    const canvasRef = useRef<HTMLCanvasElement>(null);
    const [route, setRoute] = useState<Route | null>(null);

    useEffect(() => {
        // Example API call - replace with your actual endpoint
        const fetchRoute = async () => {
            try {
                const response = await axios.get('http://localhost:5000/api/tsp/route');
                setRoute(response.data);
            } catch (error) {
                console.error('Failed to fetch route:', error);
            }
        };

        fetchRoute();
    }, []);

    useEffect(() => {
        if (!canvasRef.current || !route) return;

        const canvas = canvasRef.current;
        const ctx = canvas.getContext('2d');
        if (!ctx) return;

        // Clear canvas
        ctx.clearRect(0, 0, canvas.width, canvas.height);

        // Draw points
        route.points.forEach(point => {
            ctx.beginPath();
            ctx.arc(point.x * canvas.width, point.y * canvas.height, 5, 0, 2 * Math.PI);
            ctx.fillStyle = '#2563eb'; // blue-600
            ctx.fill();
            ctx.closePath();
        });

        // Draw connections
        ctx.beginPath();
        route.connections.forEach(([fromIdx, toIdx]) => {
            const from = route.points[fromIdx];
            const to = route.points[toIdx];
            ctx.moveTo(from.x * canvas.width, from.y * canvas.height);
            ctx.lineTo(to.x * canvas.width, to.y * canvas.height);
        });
        ctx.strokeStyle = '#dc2626'; // red-600
        ctx.lineWidth = 2;
        ctx.stroke();
    }, [route]);

    return (
        <div className="flex flex-col items-center gap-4">
            <h1 className="text-2xl font-bold text-gray-800">TSP Route Visualization</h1>
            <canvas
                ref={canvasRef}
                width={600}
                height={400}
                className="border-2 border-gray-200 rounded-lg"
            />
            <div className="flex gap-2">
                <button
                    className="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700"
                    onClick={() => {/* Add refresh logic */}}
                >
                    Refresh Route
                </button>
            </div>
        </div>
    );
};

export default TSPVisualizer;