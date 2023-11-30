import { useEffect, useState } from 'react';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

export default function useSignalr(): { connection: HubConnection | undefined } {
  const [connection, setConnection] = useState<HubConnection>();

  useEffect(() => {
    const connect = new HubConnectionBuilder()
      .withUrl("https://localhost:44394/notify")
      .withAutomaticReconnect()
      .build();
    setConnection(connect);
  }, []);
  return { connection };
}
