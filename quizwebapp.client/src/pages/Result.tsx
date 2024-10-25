import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { BACKEND_URL } from "../constants";
import { QuizResult } from "../Schemas";
import { toast } from "react-toastify";

const Result = () => {
  const resultId = useParams().id;
  const [result, setResult] = useState<QuizResult | null>(null);

  useEffect(() => {
    fetchResult();
  }, []);

  const fetchResult = async () => {
    // Fetch result by ID
    const resultURL = `${BACKEND_URL}/api/takeanswers/${resultId}`;
    const response = await fetch(resultURL);
    if (response.ok) {
      const data = await response.json();
      setResult(data);
    } else {
      console.error("Failed to fetch result");
      toast.error("Failed to fetch result");
    }
  };
  return (
    <div className="container mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <h1 className="text-2xl font-semibold">Quiz Results</h1>
        {result ? (
            <div className="mt-4">
            <h2 className="text-xl font-semibold">{result.quizTitle}</h2>
            <p className="text-lg">Score: {result.score}/ {result.totalQuestion}</p>
            <p className="text-lg">Start Time: {result.startTime}</p>
            <p className="text-lg">End Time: {result.submitTime}</p>
            <p className="text-lg">Time taken: {result.timeTaken} seconds</p>
            <div className="mt-4">
                {result.results.map((answer) => (
                <div key={answer.questionId} className="mb-4">
                    <p className="text-sm">Your answer: {answer.answerText}</p>
                    <p className="text-sm">Correct answer: {answer.correctAnswerText}</p>
                </div>
                ))}
            </div>
            </div>
        ) : (
            <div>Loading...</div>
        )}
    </div>
  );
};

export default Result;
