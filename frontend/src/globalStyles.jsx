import { createGlobalStyle } from "styled-components";

const GlobalStyles = createGlobalStyle`

  #page {
      
    display: flex;
    flex-direction: column;
    min-height: 100vh;
    overflow-x: hidden;
    font-size: 12px;
  }
  html, body {
    background-color: #F6F6FD;
    max-width: 100%;
    font-family: 'Rubik', sans-serif;
    ${'' /* background: #F6F6FD; */}
    color: #092896;

}

${'' /* @font-face {
  font-family: 'Montserrat';
  src: local('Montserrat'), url(./fonts/Montserrat/Montserrat-VariableFont_wght.ttf) format('truetype');
} */}



  #root {
    flex-grow: 1;
    align-items: stretch;
    height: auto;
  }

  .nowrap {
    white-space: nowrap;
  }
`;

export default GlobalStyles;
