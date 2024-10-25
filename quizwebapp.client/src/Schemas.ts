interface User {
  email: string;
  password: string;
  username: string;
}

interface Quiz {
  quizId: number;
  quizTitle: string;
  timeLimit: number;
  createdAt: Date;
  updatedAt: Date;
  userId: number;
  questions: Question[];
}

interface Question {
  questionId: number;
  questionText: string;
  createdAt: Date;
  updatedAt: Date;
  answers: Answer[];
}

interface Answer {
  answerId: number;
  answerText: string;
  isCorrect: boolean;
}

interface CreateQuestionObj {
  quizId: number;
  questionText: string;
  answers: CreateAnswerObj[];
}

interface CreateAnswerObj {
  answerText: string;
  isCorrect: boolean;
}

interface TakeAnswer {
  answerId: number;
  answerText: string;
}

interface TakeQuestion {
  questionId: number;
  questionText: string;
  takeAnswerDtos: TakeAnswer[];
}

interface TakeQuizObj {
  takeId: number;
  quizId: number;
  quizTitle: string;
  startedAt: string;
  timeLimit: number;
  totalQuestion: number;
  questions: TakeQuestion[];
}

interface QuizResult {
  takeId: number;
  quizId: number;
  quizTitle: string;
  score: number;
  totalQuestion: number;
  startTime: string;
  submitTime: string;
  timeTaken: number;
  results: Result[];
}

interface Result {
  questionId: number;
  answerId: number;
  answerText: string;
  correctAnswerId: number;
  correctAnswerText: string;
  isCorrect: boolean;
}

export type {
  User,
  Quiz,
  Question,
  Answer,
  CreateQuestionObj,
  CreateAnswerObj,
  TakeAnswer,
  TakeQuestion,
  TakeQuizObj as TakeQuiz,
  QuizResult,
  Result,
};
