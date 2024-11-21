export { };

declare global {
  interface Window {
    loggedIn: string | null;
    nickname: string | null;
  }
}

window.loggedIn = "boss";
window.nickname = "testBoss";