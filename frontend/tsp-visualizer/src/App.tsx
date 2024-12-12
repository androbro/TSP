import './App.css'
import TSPVisualizer from "./components/Visualizer.tsx";
import RouteDisplay from "./components/RouteDisplay.tsx";

function App() {
    return (
        <div className="min-h-screen bg-gray-50 p-8">
            <TSPVisualizer />
            <RouteDisplay />
        </div>
    )
}

export default App