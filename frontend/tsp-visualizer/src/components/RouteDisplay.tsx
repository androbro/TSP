import React from 'react';
import { RouteDto } from '../services/api';

interface RouteDisplayProps {
    route: RouteDto;
}

export const RouteDisplay: React.FC<RouteDisplayProps> = ({ route }) => (
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
);