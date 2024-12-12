import { useEffect, useRef, useState } from 'react';
import { mapService, PointDto } from '../services/api';

const GridVisualizer = () => {
    const canvasRef = useRef<HTMLCanvasElement | null>(null);
    const [points, setPoints] = useState<PointDto[]>([]);
    const [gridSize, setGridSize] = useState(1000);
    const [numPoints, setNumPoints] = useState(12);
    const [loading, setLoading] = useState(false);

    // Preset grid sizes
    const GRID_SIZE_OPTIONS = [
        { value: 500, label: '750 x 750' },
        { value: 1000, label: '1000 x 1000' },
        { value: 1500, label: '1250 x 1250' },
    ];

    const DESIRED_CELL_COUNT = 20;

    const drawGrid = (ctx: CanvasRenderingContext2D, width: number, height: number) => {
        ctx.strokeStyle = '#e5e7eb';
        ctx.lineWidth = 1;

        const cellSize = width / DESIRED_CELL_COUNT;

        // Draw vertical lines
        for (let x = 0; x <= width; x += cellSize) {
            ctx.beginPath();
            ctx.moveTo(x, 0);
            ctx.lineTo(x, height);
            ctx.stroke();
        }

        // Draw horizontal lines
        for (let y = 0; y <= height; y += cellSize) {
            ctx.beginPath();
            ctx.moveTo(0, y);
            ctx.lineTo(width, y);
            ctx.stroke();
        }

        // Draw coordinates at grid intersections
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

            // Draw point index and coordinates
            ctx.fillStyle = '#1f2937';
            ctx.font = '12px sans-serif';
            ctx.fillText(`${index + 1} (${point.x},${point.y})`, point.x + 10, point.y + 10);
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
                    <label className="block text-sm font-medium text-gray-700">
                        Number of Points
                    </label>
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