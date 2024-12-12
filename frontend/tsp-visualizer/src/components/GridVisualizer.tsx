import { useEffect, useRef, useState } from 'react';
import { mapService, PointDto } from '../services/api';

const GridVisualizer = () => {
    const canvasRef = useRef<HTMLCanvasElement | null>(null);
    const [points, setPoints] = useState<PointDto[]>([]);
    const [gridSize, setGridSize] = useState(1000);
    const [numPoints, setNumPoints] = useState(12);
    const [loading, setLoading] = useState(false);

    const drawGrid = (ctx: CanvasRenderingContext2D, width: number, height: number) => {
        ctx.strokeStyle = '#e5e7eb';
        ctx.lineWidth = 1;

        // Draw vertical lines
        for (let x = 0; x <= width; x += 50) {
            ctx.beginPath();
            ctx.moveTo(x, 0);
            ctx.lineTo(x, height);
            ctx.stroke();
        }

        // Draw horizontal lines
        for (let y = 0; y <= height; y += 50) {
            ctx.beginPath();
            ctx.moveTo(0, y);
            ctx.lineTo(width, y);
            ctx.stroke();
        }
    };

    const drawPoints = (ctx: CanvasRenderingContext2D) => {
        ctx.fillStyle = '#3b82f6';
        points.forEach((point, index) => {
            ctx.beginPath();
            ctx.arc(point.x, point.y, 6, 0, 2 * Math.PI);
            ctx.fill();

            // Draw point index
            ctx.fillStyle = '#1f2937';
            ctx.font = '12px sans-serif';
            ctx.fillText(String(index + 1), point.x + 10, point.y + 10);
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

    useEffect(() => {
        const canvas = canvasRef.current;
        if (!canvas) return;

        const ctx = canvas.getContext('2d');
        if (!ctx) return;

        // Clear canvas
        ctx.clearRect(0, 0, gridSize, gridSize);

        // Draw grid and points
        drawGrid(ctx, gridSize, gridSize);
        drawPoints(ctx);
    }, [points, gridSize]);

    return (
        <div className="p-4 space-y-4">
            <div className="flex gap-4 items-end">
                <div className="space-y-2">
                    <label className="block text-sm font-medium text-gray-700">
                        Grid Size
                    </label>
                    <input
                        type="number"
                        value={gridSize}
                        onChange={(e) => setGridSize(Number(e.target.value))}
                        className="block w-32 rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                    />
                </div>

                <div className="space-y-2">
                    <label className="block text-sm font-medium text-gray-700">
                        Number of Points
                    </label>
                    <input
                        type="number"
                        value={numPoints}
                        onChange={(e) => setNumPoints(Number(e.target.value))}
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