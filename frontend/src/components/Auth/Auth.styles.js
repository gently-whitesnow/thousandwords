import styled from "styled-components";

export const AuthWrapper = styled.div`
  margin-top: 200px;
  max-width: 380px;
  display: flex;
  flex-direction: column;
  *:focus {
    outline: none;
  }
`;

export const Input = styled.input`
  font-size: 18px;
  border-radius: 5px;
  color: #db98ff;
  border: none;
  min-height: 60px;
  box-shadow: rgba(0, 0, 0, 0.06) 0px 2px 4px 0px inset;
  padding-left: 10px;
  :focus {
    border: none;
  }
  ::placeholder {
    /* Most modern browsers support this now. */
    color: #db98ff;
    opacity: 0.5;
  }
  background-color: ${(props) =>
    props.value ? (props.valid ? "#e4ffdb" : "#ffd1d1") : "white"};
`;

export const Text = styled.div`
  color: #8eb4ff;
  font-size: 16px;
  margin-bottom: 10px;
  text-align: justify;
`;

export const NextButton = styled.button`
  cursor: pointer;
  border: none;
  color: white;
  font-size: 30px;
  min-height: 60px;
  background-color: #98ff75;
`;
