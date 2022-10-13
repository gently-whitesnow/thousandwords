import styled from "styled-components";

export const MindWordWrapper = styled.div`
  cursor: pointer;
  display: flex;
  justify-content: center;
  height: 100%;
  width: 100%;
  margin-top: 20px;
  margin-bottom: 20px;
  @media (max-width: 768px) {
    margin-bottom: 10px;
    margin-top: 10px;
  }
`;

export const CustomWord = styled.div`
  font-size: 70px;
  color: #8eb4ff;
  text-shadow: 0px 0px 9px rgba(150, 150, 150, 0.3);

  @media (max-width: 768px) {
    font-size: 30px;
  }
`;
