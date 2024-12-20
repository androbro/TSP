import React from 'react';

interface GridControlsProps {
    gridSize: number;
    numPoints: number;
    onGridSizeChange: (size: number) => void;
    onNumPointsChange: (num: number) => void;
    onGeneratePoints: () => void;
    loading: boolean;
}

export const GridControls: React.FC<GridControlsProps> = ({
                                                              gridSize,
                                                              numPoints,
                                                              onGridSizeChange,
                                                              onNumPointsChange,
                                                              onGeneratePoints,
                                                              loading
                                                          }) => {
    const GRID_SIZE_OPTIONS = [
        { value: 500, label: '750 x 750' },
        { value: 1000, label: '1000 x 1000' },
        { value: 1500, label: '1250 x 1250' },
    ];

    return (
        <div className="flex gap-4 items-end">
            <div className="space-y-2">
                <label className="block text-sm font-medium text-gray-700">Grid Size</label>
                <select
                    value={gridSize}
                    onChange={(e) => onGridSizeChange(Number(e.target.value))}
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
                    onChange={(e) => onNumPointsChange(Number(e.target.value))}
                    min={2}
                    max={50}
                    className="block w-32 rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                />
            </div>

            <button
                onClick={onGeneratePoints}
                disabled={loading}
                className="px-4 py-2 bg-blue-500 text-white rounded-md hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 disabled:opacity-50"
            >
                {loading ? 'Generating...' : 'Generate Points'}
            </button>
        </div>
    );
};