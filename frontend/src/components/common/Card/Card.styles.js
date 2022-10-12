import styled from "styled-components";

export const AnimationWrapper = styled.div`
  height: 100%;
  width: 100%;
  margin: 30px;
  .success {
    animation: color-success 1s;
  }
  .notsuccess {
    animation: color-notsuccess 1s;
  }
  @keyframes color-success {
    0% {
      background-color: #ffdf69;
    }
    40% {
      background-color: #bbffa5;
      box-shadow: #bbffa5 0px
          1px 2px 0px,
        rgba(60, 64, 67, 0.15) 0px 2px 6px 2px;
    }
    100% {
      background-color: #ffdf69;
    }
  }
  @keyframes color-notsuccess {
    0% {
      background-color: #ffdf69;
    }
    40% {
      background-color: #ff6969;
      box-shadow: #ff6969 0px
          1px 2px 0px,
        rgba(60, 64, 67, 0.15) 0px 2px 6px 2px;
    }
    100% {
      background-color: #ffdf69;
    }
  }
`;

export const CardWrapper = styled.button`
  cursor: pointer;
  max-height: 256px;
  min-height: 128px;
  max-width: 512px;
  height: 100%;
  width: 100%;

  background-color: #ffe48f; //#fec85a;
  border: none;

  border-radius: 20px;
  transition: all 0.2s ease-in-out;
  :hover {
    background-color: #ffdf69; //#fec85a;
    transform: scale(1.05);
    box-shadow: #fff18b 0px 25px 20px -20px;
  }

  display: flex;
  align-items: center;
  justify-content: center;

  @media (max-width: 768px) {
    margin: 15px;
    font-size: 20px;
  }
  color: white;
  font-size: 40px;
  text-align: center;
`;
