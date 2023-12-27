import { useEffect, useState } from 'react';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

interface Props {
  url: string;
}

export default function useSignalr(): { connection: HubConnection | undefined } {
  const [connection, setConnection] = useState<HubConnection>();

  useEffect(() => {
    const connect = new HubConnectionBuilder()
      .withUrl(process.env.REACT_APP_BASE_API + 'notify')
      .withAutomaticReconnect()
      .build();
    setConnection(connect);
  }, []);
  return { connection };
}
