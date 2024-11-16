import React, { FC, useState } from 'react';
import {
   LoginPageWrapper,
   LoginPageContainer
} from './LoginPage.styled.ts';
import './LoginPage.css';

import { Form, Button, Container } from "react-bootstrap";
import 'bootstrap/dist/css/bootstrap.min.css';

interface LoginPageProps { }

const LoginPage: FC<LoginPageProps> = () => {
   const [username, setUsername] = useState("");
   const [password, setPassword] = useState("");

   const handleSubmit = (e: React.FormEvent) => {
      e.preventDefault();

      // TODO api запрос для входу в акк + запис значень у змінні window.loggedIn та window.nickname
   };

   return (
      <LoginPageWrapper>
         <LoginPageContainer>
            <Form onSubmit={handleSubmit} style={{ width: "300px" }}>
               <Form.Group controlId="formUsername" className="mb-3">
                  <Form.Control
                     className='FormPlaceholder'
                     style={{
                        backgroundColor: "rgb(33, 37, 41)",
                        color: "white",
                        border: "none"
                     }}
                     type="text"
                     placeholder="Enter username"
                     value={username}
                     onChange={(e) => setUsername(e.target.value)}
                     required
                  />
               </Form.Group>
               <Form.Group controlId="formPassword" className="mb-3">
                  <Form.Control
                     className='FormPlaceholder'
                     style={{
                        backgroundColor: "rgb(33, 37, 41)",
                        color: "white",
                        border: "none"
                     }}
                     type="password"
                     placeholder="Enter password"
                     value={password}
                     onChange={(e) => setPassword(e.target.value)}
                     required
                  />
               </Form.Group>
               <Button className="w-100" variant="dark" type="submit">
                  Login
               </Button>
            </Form>
         </LoginPageContainer>
      </LoginPageWrapper>
   );
};

export default LoginPage;
