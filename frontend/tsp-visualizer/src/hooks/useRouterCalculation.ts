﻿import { useState } from 'react';
import {PointDto, RouteDto, routeService} from "../services/api.ts";

export const useRouteCalculation = () => {
    const [route, setRoute] = useState<RouteDto | null>(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const calculateRoute = async (points: PointDto[]) => {
        try {
            setLoading(true);
            setError(null);
            const pointDtoList = points.map((point) => ({
                id: point.id,
                x: point.x,
                y: point.y
            }));
            const result = await routeService.calculateRoute({
                points: pointDtoList,
                algorithm: 0
            });
            setRoute(result);
        } catch (err) {
            setError('Failed to calculate route');
            console.error(err);
        } finally {
            setLoading(false);
        }
    };

    return { route, loading, error, calculateRoute };
};