import styled from "styled-components";

export const LevelBarWrapper = styled.div`
  position: relative;
  max-height: 256px;
  min-height: 32px;
  max-width: 800px;
  height: 100%;
  width: 100%;
  margin-top: 20px;
`;

export const BarGround = styled.div`
  border-radius: 10px;
  min-height: 16px;
  /* border: solid #db98ff 2px; */
  position: absolute;
  max-width: 800px;
  width: 100%;
  box-shadow: rgb(204, 219, 232) 3px 3px 6px 0px inset,
    rgba(255, 255, 255, 0.5) -3px -3px 6px 1px inset;
`;

export const Bar = styled.div`
  border-radius: 10px;
  position: absolute;
  max-width: 800px;
  min-height: 16px;
  width: 50%;
  background-color: #bbffa5;
  
`;

export const BarLighter = styled.div`
  border-radius: 20px;
  position: absolute;
  max-width: 800px;
  min-height: 16px;
  width: 60%;
  opacity: 0.5;
  background-color: #bbffa5;
`;
