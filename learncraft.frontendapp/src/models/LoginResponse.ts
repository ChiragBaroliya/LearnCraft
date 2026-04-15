type LoginResponse = {
    data: {
        login: string;
    };
    errors?: {
        message: string;
    }[];
};

export default LoginResponse;