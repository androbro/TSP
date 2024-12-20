import { useEffect, RefObject } from 'react';
import {PointDto, RouteDto} from '../services/api';
import {drawConnections, drawGrid, drawPoints} from '../utils/canvasUtils';

export const useCanvas = (
    canvasRef: RefObject<HTMLCanvasElement>,
    points: PointDto[],
    gridSize: number,
    route: RouteDto | null
) => {
    useEffect(() => {
        const canvas = canvasRef.current;
        if (!canvas) return;

        const ctx = canvas.getContext('2d');
        if (!ctx) return;

        ctx.clearRect(0, 0, gridSize, gridSize);
        drawGrid(ctx, gridSize, gridSize);

        // Draw connections if route exists
        if (route?.connections) {
            console.log('Drawing connections:', route.connections);
            drawConnections(ctx, route.connections);
        } else {
            console.log('No connections to draw');
        }

        drawPoints(ctx, points);
    }, [points, gridSize, canvasRef, route]);
};