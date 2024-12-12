import {useState} from "react";
import {RouteDto, routeService} from "../services/api.ts";

interface RouteDisplayProps {
}

export default function RouteDisplay({}: RouteDisplayProps) {
    const [route, setRoute] = useState<RouteDto | null>(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const calculateRoute = async () => {
        try {
            setLoading(true);
            setError(null);
            const result = await routeService.calculateRoute();
            setRoute(result);
        } catch (err) {
            setError('Failed to calculate route');
            console.error(err);
        } finally {
            setLoading(false);
        }
    };

    return (
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
    );
}