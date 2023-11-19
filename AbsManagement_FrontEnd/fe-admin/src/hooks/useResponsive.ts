import { Grid } from 'antd';

export type Breakpoint = 'xxl' | 'xl' | 'lg' | 'md' | 'sm' | 'xs';
const { useBreakpoint } = Grid;
export const useResponsive = (): Partial<Record<Breakpoint, boolean>> => {
    const screens = useBreakpoint();
    return screens;
};
