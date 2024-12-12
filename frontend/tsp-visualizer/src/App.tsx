import './App.css'
import TSPVisualizer from "./components/Visualizer.tsx";
import RouteDisplay from "./components/RouteDisplay.tsx";
import GridVisualizer from "./components/GridVisualizer.tsx";

function App() {
    return (
        <div className="min-h-screen bg-gray-50 p-8">
            <TSPVisualizer/>
            <GridVisualizer/>
            <RouteDisplay/>
        </div>
    )
}

export default App