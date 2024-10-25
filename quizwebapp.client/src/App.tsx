import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Home from './pages/Home';
import NavBar from './components/NavBar';
import Auth from './pages/Auth';
import CreateQuiz from './pages/CreateQuiz';
import CreateQuestion from './pages/CreateQuestion';
import TakeQuiz from './pages/TakeQuiz';
import Result from './pages/Result';
import Results from './pages/Results';
function App() {
    return <>
        <ToastContainer closeOnClick autoClose={2000} />
        <NavBar />
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/auth" element={<Auth/>} />
                <Route path="/create-question" element={<CreateQuestion/>} />
                <Route path="/create-quiz" element={<CreateQuiz/>} />
                <Route path="/quiz/attemp/:id" element={<TakeQuiz/>} />
                <Route path="/results" element={<Results/>} />
                <Route path="/results/:id" element={<Result/>} />
                <Route path="/quiz/attemp/:id" element={<TakeQuiz/>} />
                <Route path="*" element={<div>Not Found</div>} />
            </Routes>
        </BrowserRouter>
    </>
}

export default App;