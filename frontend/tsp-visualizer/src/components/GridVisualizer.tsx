import { useEffect, useRef, useState } from 'react';
import {mapService, PointDto, RouteDto, routeService} from '../services/api';

const GridVisualizer = () => {
    const canvasRef = useRef<HTMLCanvasElement | null>(null);
    const [points, setPoints] = useState<PointDto[]>([]);
    const [gridSize, setGridSize] = useState(1000);
    const [numPoints, setNumPoints] = useState(12);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [route, setRoute] = useState<RouteDto | null>(null);

    const GRID_SIZE_OPTIONS = [
        { value: 500, label: '750 x 750' },
        { value: 1000, label: '1000 x 1000' },
        { value: 1500, label: '1250 x 1250' },
    ];

    const drawGrid = (ctx: CanvasRenderingContext2D, width: number, height: number) => {
        const cellSize = width / 20;
        ctx.strokeStyle = '#e5e7eb';
        ctx.lineWidth = 1;

        for (let x = 0; x <= width; x += cellSize) {
            ctx.beginPath();
            ctx.moveTo(x, 0);
            ctx.lineTo(x, height);
            ctx.stroke();
        }

        for (let y = 0; y <= height; y += cellSize) {
            ctx.beginPath();
            ctx.moveTo(0, y);
            ctx.lineTo(width, y);
            ctx.stroke();
        }

        ctx.fillStyle = '#9ca3af';
        ctx.font = '10px sans-serif';
        for (let x = 0; x <= width; x += cellSize) {
            for (let y = 0; y <= height; y += cellSize) {
                if (x % (cellSize * 4) === 0 && y % (cellSize * 4) === 0) {
                    ctx.fillText(`(${Math.round(x)},${Math.round(y)})`, x + 2, y + 12);
                }
            }
        }
    };

    const drawPoints = (ctx: CanvasRenderingContext2D) => {
        ctx.fillStyle = '#3b82f6';
        points.forEach((point, index) => {
            ctx.beginPath();
            ctx.arc(point.x, point.y, 6, 0, 2 * Math.PI);
            ctx.fill();

            ctx.fillStyle = '#fff';
            ctx.font = '12px sans-serif';
            ctx.fillText(`${index + 1}`, point.x - 4, point.y + 4);
            ctx.fillStyle = '#3b82f6';
        });
    };

    const generatePoints = async () => {
        try {
            setLoading(true);
            const response = await mapService.generateMap({
                gridSize,
                numberOfPoints: numPoints
            });
            setPoints(response);
        } catch (error) {
            console.error('Failed to generate points:', error);
        } finally {
            setLoading(false);
        }
    };

    const calculateRoute = async () => {
        try {
            setLoading(true);
            setError(null);
            const pointDtoList: PointDto[] = points.map((point) => ({
                id: point.id,
                x: point.x,
                y: point.y
            }));
            const result = await routeService.calculateRoute({points: pointDtoList, algorithm:0});
            setRoute(result);
        } catch (err) {
            setError('Failed to calculate route');
            console.error(err);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        const canvas = canvasRef.current;
        if (!canvas) return;

        const ctx = canvas.getContext('2d');
        if (!ctx) return;

        ctx.clearRect(0, 0, gridSize, gridSize);
        drawGrid(ctx, gridSize, gridSize);
        drawPoints(ctx);
    }, [points, gridSize]);

    return (
        <div className="p-4 space-y-4">
            <div className="flex gap-4 items-end">
                <div className="space-y-2">
                    <label className="block text-sm font-medium text-gray-700">Grid Size</label>
                    <select
                        value={gridSize}
                        onChange={(e) => setGridSize(Number(e.target.value))}
                        className="block w-40 rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                    >
                        {GRID_SIZE_OPTIONS.map(option => (
                            <option key={option.value} value={option.value}>
                                {option.label}
                            </option>
                        ))}
                    </select>
                </div>

                <div className="space-y-2">
                    <label className="block text-sm font-medium text-gray-700">Number of Points</label>
                    <input
                        type="number"
                        value={numPoints}
                        onChange={(e) => setNumPoints(Number(e.target.value))}
                        min={2}
                        max={50}
                        className="block w-32 rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                    />
                </div>

                <button
                    onClick={generatePoints}
                    disabled={loading}
                    className="px-4 py-2 bg-blue-500 text-white rounded-md hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 disabled:opacity-50"
                >
                    {loading ? 'Generating...' : 'Generate Points'}
                </button>
            </div>
            <div className="p-4">
                <button
                    onClick={calculateRoute}
                    disabled={loading}
                    className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded disabled:opacity-50"
                >
                    {loading ? 'Calculating...' : 'Calculate Route'}
                </button>

                {error && (
                    <div className="mt-4 text-red-500">
                        {error}
                    </div>
                )}

                {route && (
                    <div className="mt-4">
                        <h2 className="text-xl font-bold mb-2">Route Details</h2>
                        <div className="space-y-2">
                            <p>Total Distance: {route.totalDistance}</p>
                            <p>Calculation Time: {route.calculationTime}</p>
                            <h3 className="font-bold">Points:</h3>
                            <ul className="list-disc pl-5">
                                {route.points.map((point, index) => (
                                    <li key={index}>
                                        ({point.x}, {point.y})
                                    </li>
                                ))}
                            </ul>
                        </div>
                    </div>
                )}
            </div>

            <canvas
                ref={canvasRef}
                width={gridSize}
                height={gridSize}
                className="border border-gray-300 rounded-lg"
            />
        </div>
    );
};

export default GridVisualizer;