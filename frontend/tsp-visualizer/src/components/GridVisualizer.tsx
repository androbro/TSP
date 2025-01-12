// GridVisualizer.tsx
import { useRef, useState } from 'react';
import { PointDto } from '../services/api';
import { mapService } from '../services/api';
import { useCanvas } from '../hooks/useCanvas';
import { GridControls } from './GridControls';
import { RouteDisplay } from './RouteDisplay';
import {useRouteCalculation} from "../hooks/useRouterCalculation.ts";

const GridVisualizer = () => {
    const canvasRef = useRef<HTMLCanvasElement | null>(null);
    const [points, setPoints] = useState<PointDto[]>([]);
    const [gridSize, setGridSize] = useState(1000);
    const [numPoints, setNumPoints] = useState(12);
    const [algorithm, setAlgorithm] = useState(1);
    const [loading, setLoading] = useState(false);

    const { route, error, loading: routeLoading, calculateRoute } = useRouteCalculation();

    useCanvas(canvasRef, points, gridSize, route);

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

    return (
        <div className="p-4 space-y-4">
            <GridControls
                gridSize={gridSize}
                numPoints={numPoints}
                algorithm={algorithm}
                onGridSizeChange={setGridSize}
                onNumPointsChange={setNumPoints}
                onAlgorithmChange={setAlgorithm}
                onGeneratePoints={generatePoints}
                loading={loading}
            />

            <div className="p-4">
                <button
                    onClick={() => calculateRoute(points, algorithm)}
                    disabled={routeLoading || points.length === 0}
                    className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded disabled:opacity-50"
                >
                    {routeLoading ? 'Calculating...' : 'Calculate Route'}
                </button>

                {error && (
                    <div className="mt-4 text-red-500">
                        {error}
                    </div>
                )}

                {route && <RouteDisplay route={route} />}
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