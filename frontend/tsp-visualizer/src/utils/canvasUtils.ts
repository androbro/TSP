import {ConnectionDto, PointDto} from '../services/api';

export const drawGrid = (
    ctx: CanvasRenderingContext2D,
    width: number,
    height: number
) => {
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

export const drawConnections = (ctx: CanvasRenderingContext2D, connections: ConnectionDto[]) => {
    connections.forEach(connection => {
        ctx.beginPath();
        ctx.moveTo(connection.fromPoint.x, connection.fromPoint.y);
        ctx.lineTo(connection.toPoint.x, connection.toPoint.y);

        // Style based on whether the connection is optimal
        if (connection.isOptimal) {
            ctx.strokeStyle = '#10b981'; // Green for optimal connections
            ctx.lineWidth = 2;
        } else {
            ctx.strokeStyle = '#6b7280'; // Gray for non-optimal connections
            ctx.lineWidth = 1;
        }

        ctx.stroke();
    });
};

export const drawPoints = (ctx: CanvasRenderingContext2D, points: PointDto[]) => {
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