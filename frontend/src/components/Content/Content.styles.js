import styled from "styled-components";

export const ContentWrapper = styled.div`
  display: flex;
  align-items: center;
  flex-direction: column;
  height: 100%;
  width: 100%;
  margin-top: 100px;
  max-height: 1024px;

  @media (max-width: 768px) {
    margin-top: 40px;
  }
`;


