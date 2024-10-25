import React, { useEffect, useState } from "react";
import { QuizResult } from "../Schemas";
import { useNavigate } from "react-router-dom";

const Results = () => {
  const [results, setResults] = useState<QuizResult[]>([]);
  const [takeId, setTakeId] = useState(0);
    const navigate = useNavigate();
  useEffect(() => {
    fetchResults();
  }, []);

  const fetchResults = async () => {
    const response = await fetch("https://localhost:7210/api/takeanswers");
    if (response.ok) {
      const data = await response.json();
      // Only have unique TakeId
        const uniqueResults = Array.from(new Set(data.map((a: any) => a.takeId))).map((takeId) => {
            return data.find((a: any) => a.takeId === takeId);
        });
        setResults(uniqueResults);
    }
  };


  return (
    <div className="container mx-auto px-4 sm:px-6 lg:px-8 py-8">
      <h1> Results </h1>
      <div className="overflow-x-auto shadow-md sm:rounded-lg">
        <table className="w-full text-sm text-left text-gray-500">
          <thead className="text-xs text-gray-700 uppercase bg-gray-50">
            <tr>
              <th className="p-2">Quiz Title</th>
              <th className="p-2">Score</th>
              <th className="p-2">Total Questions</th>
              <th className="p-2">Start Time</th>
              <th className="p-2">Submit Time</th>
              <th className="p-2">Time Taken</th>
              <th className="p-2">Actions</th>
            </tr>
          </thead>
          <tbody>
            {results.map((result) => (
              <tr>
                <td className="p-2">{result.quizTitle}</td>
                <td className="p-2">{result.score}</td>
                <td className="p-2">{result.results.length}</td>
                <td className="p-2">{result.startTime}</td>
                <td className="p-2">{result.submitTime}</td>
                <td className="p-2">{result.timeTaken} seconds</td>
                <td className="p-2">
                  <button className="p-2 bg-blue-500 text-white rounded-md"
                  onClick={() => {
                    navigate(`/results/${result.takeId}`);
                  }}
                  >
                    View
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default Results;
