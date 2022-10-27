import styled from "styled-components";

export const CardHolderWrapper = styled.div`
  max-height: 1024px;
  max-width: 1024px;
  height: 100%;
  width: 100%;
`;

export const Line = styled.div`
  max-height: 512px;
  min-height: 200px;
  height: 100%;
  width: 100%;
  display: flex;
  margin:0;


  @media (max-width: 768px) {
    flex-direction: column;
    justify-content: space-around;
  }
`;
