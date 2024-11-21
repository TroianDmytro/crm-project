import React, { FC, ReactNode } from 'react';

import PageHeader from '../elements/PageHeader/PageHeader.tsx'

interface AppProps { 
   children: ReactNode;
}

const App: FC<AppProps> = ({ children }) => {
   return (
      <>
         <PageHeader />
         <main>{children}</main>
      </>
   );
};
export default App;