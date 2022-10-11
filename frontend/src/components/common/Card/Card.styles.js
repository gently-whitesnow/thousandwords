import styled from "styled-components";

export const CardWrapper = styled.div`
  max-height: 256px;
  min-height: 128px;
  max-width: 512px;
  height: 100%;
  width: 100%;
  margin: 30px;
  background-color: #ffe48f; //#fec85a;

  border-radius: 10px;
  transition: all 0.2s ease-in-out;
  :hover {
    /* background-color: #faff89; //#fec85a; */
    transform: scale(1.05);
    box-shadow: #fff18b 0px 25px 20px -20px;
  }
  display: flex;
  align-items: center;
  justify-content: center;
`;

export const Word = styled.div`
  color: white;
  font-size: 40px;
  text-align: center;
  
`;
