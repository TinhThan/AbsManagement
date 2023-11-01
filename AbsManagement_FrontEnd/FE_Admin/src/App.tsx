import React from 'react';
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';

const App: React.FC = () => {
  return (
    <Router>
      <Routes>
        {/* <Route path="/login" element={<Login />} />
        <Route path="/home" element={<Home />} />
        <Route path="/actors/:id" element={<ActorDetail />} />
        <Route path="/actors" element={<ActorList />} />
        <Route path="/" element={<Navigate to="/home" />} /> */}
      </Routes>
    </Router>
  );
};

export default App;