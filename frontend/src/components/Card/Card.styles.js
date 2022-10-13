import styled from "styled-components";

export const AnimationWrapper = styled.div`
  /* height: 100%; */
  margin: 30px;
  width: 100%;
  display: flex;
  justify-content: center;
  @media (max-width: 768px) {
    margin: 0px;
  }
  .success {
    animation: color-success 1s;
  }
  .notsuccess {
    animation: color-notsuccess 1s;
  }
  @keyframes color-success {
    0% {
      background-color: #ffd61f;
    }
    40% {
      background-color: #bbffa5;
      box-shadow: #bbffa5 0px 1px 2px 0px,
        rgba(60, 64, 67, 0.15) 0px 2px 6px 2px;
    }
    100% {
      background-color: #ffd61f;
    }
  }
  @keyframes color-notsuccess {
    0% {
      background-color: #ffd61f;
    }
    40% {
      background-color: #ff6969;
      box-shadow: #ff6969 0px 1px 2px 0px,
        rgba(60, 64, 67, 0.15) 0px 2px 6px 2px;
    }
    100% {
      background-color: #ffd61f;
    }
  }
`;

export const CardWrapper = styled.div`
  -webkit-tap-highlight-color: transparent;
  cursor: pointer;
  max-height: 256px;
  min-height: 70px;
  height: 100%;
  max-width: 400px;
  width: 100%;

  background-color: #ffdb44; 
  border: none;

  border-radius: 20px;
  transition: all 0.2s ease-in-out;
  :hover {
    background-color: #ffd61f; 
    transform: scale(1.05);
    box-shadow: #fff18b 0px 25px 20px -20px;
  }

  display: flex;
  align-items: center;
  justify-content: center;

  color: white;
  font-size: 40px;
  text-align: center;
  @media (max-width: 768px) {
    font-size: 20px;
  }
  @media (max-width: 1024px) {
    font-size: 30px;
  }
`;
