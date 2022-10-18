import React from "react";
import { observer } from "mobx-react-lite";
import { AuthWrapper, Input, NextButton, Text } from "./Auth.styles";
import { useStore } from "../../store";

const Auth = () => {
  const { globalStore } = useStore();

  const { email, setEmail, setValidEmail, validEmail, sendEmailhandler } =
    globalStore;
  
  const onChange = (e) => {
    let mail = e.target.value.trim();

    setEmail(mail);
    console.log(validEmail);
    setValidEmail(validateEmail(mail));
  };

  const regex = /^\w+([\\.-]?\w+)*@\w+([\\.-]?\w+)*(\.\w{2,3})+$/;
  const validateEmail = (mail) => {
    console.log(mail);
    if (regex.test(mail)) {
      return true;
    }
    return false;
  };
  return (
    <AuthWrapper>
      <Text>Введите почту, мы запомним слова</Text>
      <Input
        placeholder="email@domain.ru"
        onChange={onChange}
        value={email}
        valid={validEmail}
      />
      {validEmail ? (
        <NextButton
          onClick={() => {
            sendEmailhandler(email);
          }}
        >
          ✔
        </NextButton>
      ) : null}
    </AuthWrapper>
  );
};

export default observer(Auth);
