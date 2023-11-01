import { useEffect, useState } from 'react';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

interface Props {
  url: string;
}

export default function useSignalr({ url }: Props): { connection: HubConnection | undefined } {
  const [connection, setConnection] = useState<HubConnection>();

  useEffect(() => {
    const connect = new HubConnectionBuilder()
      .withUrl(url)
      .withAutomaticReconnect()
      .build();
    setConnection(connect);
  }, []);
  return { connection };
}
