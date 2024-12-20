import { useEffect, RefObject } from 'react';
import {drawGrid, drawPoints} from "../utils/canvasUtils.ts";
import {PointDto} from "../services/api.ts";

export const useCanvas = (
    canvasRef: RefObject<HTMLCanvasElement>,
    points: PointDto[],
    gridSize: number
) => {
    useEffect(() => {
        const canvas = canvasRef.current;
        if (!canvas) return;

        const ctx = canvas.getContext('2d');
        if (!ctx) return;

        ctx.clearRect(0, 0, gridSize, gridSize);
        drawGrid(ctx, gridSize, gridSize);
        drawPoints(ctx, points);
    }, [points, gridSize, canvasRef]);
};