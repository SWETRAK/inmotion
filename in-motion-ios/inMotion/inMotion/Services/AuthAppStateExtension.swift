//
// Created by Kamil Pietrak on 01/11/2023.
//

import Foundation

// MARK: - Email Auth Methods

extension AppState {

    public func loginWithEmailAndPasswordHttpRequest(requestData: LoginUserWithEmailAndPasswordDto,
                                                     successLoginAction: @escaping (UserInfoDto) -> Void,
                                                     failureLoginAction: @escaping (ImsHttpError) -> Void) {
        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/auth/api/email/login")!, timeoutInterval: Double.infinity)
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")

        request.httpMethod = HTTPMethods.POST.rawValue
        request.httpBody = JsonUtil.encodeJsonStringFromObject(requestData)

        let task = URLSession.shared.dataTask(with: request) { data, response, error in

            guard let data = data else {
                print(String(describing: error))
                return
            }

            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<UserInfoDto> = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpMessage<UserInfoDto>.self) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: UserInfoDto = safeImsMessage.data {
                            DispatchQueue.main.async {
                                self.user = userInfoDataSafe
                                self.logged = true
                                self.token = userInfoDataSafe.token
                            }
                            successLoginAction(userInfoDataSafe);
                        }
                    }
                    //TODO: Add validation action
                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpError.self) {
                        // TODO: Implement proper error handling
                        failureLoginAction(safeError);
                    }
                }
            }
        }
        task.resume()
    }

    public func registerUserWithEmailAndPasswordHttpRequest(registerData: RegisterUserWithEmailAndPasswordDto,
                                                            successRegisterAction: @escaping (SuccessfulRegistrationResponseDto) -> Void,
                                                            failureRegisterAction: @escaping (ImsHttpError) -> Void){

        let postData = JsonUtil.encodeJsonStringFromObject(registerData)
        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/auth/api/email/register")!,timeoutInterval: Double.infinity)
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")

        request.httpMethod = HTTPMethods.POST.rawValue
        request.httpBody = postData

        let task = URLSession.shared.dataTask(with: request) { data, response, error in

            guard let data = data else {
                print(String(describing: error))
                return
            }

            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 201)
                {
                    if let safeImsMessage: ImsHttpMessage<SuccessfulRegistrationResponseDto> = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpMessage<SuccessfulRegistrationResponseDto>.self) {
                        if let successfulRegistrationResponseSafe: SuccessfulRegistrationResponseDto = safeImsMessage.data {
                            successRegisterAction(successfulRegistrationResponseSafe)
                            // TODO: Add info about succesfull register and
                        }
                    }
                } else if (httpResponse.statusCode == 400) {

                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpError.self) {
                        failureRegisterAction(safeError)
                        // TODO: Implement proper error handling
                        print(safeError.status, safeError.errorMessage, safeError.errorType)
                    }
                }
            }
        }
        task.resume()
    }
    
    public func updateUserPasswordHttpRequest(requestData: UpdatePasswordDto,
                                              successPasswordChangeAction: @escaping (Bool) -> Void,
                                              failurePasswordChangeAction: @escaping (ImsHttpError) -> Void) {

        let postData = JsonUtil.encodeJsonStringFromObject(requestData)
        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/auth/api/email/password/update")!,timeoutInterval: Double.infinity)
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")
        request.addValue("Bearer \(self.token ?? "")", forHTTPHeaderField: "Authorization")

        request.httpMethod = HTTPMethods.PUT.rawValue
        request.httpBody = postData

        let task = URLSession.shared.dataTask(with: request) { data, response, error in

            guard let data = data else {
                print(String(describing: error))
                return
            }

            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<Bool> = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpMessage<Bool>.self) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: Bool = safeImsMessage.data {
                            successPasswordChangeAction(userInfoDataSafe);
                        }
                    }
                    //TODO: Add validation action
                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpError.self) {
                        // TODO: Implement proper error handling
                        failurePasswordChangeAction(safeError);
                    }
                }
            }
        }

        task.resume()
    }

    public func addPasswordHttpRequest(requestData: AddPasswordDto,
                                       successAddPasswordAction: @escaping (Bool) -> Void,
                                       failureAddPasswordAction: @escaping (ImsHttpError) -> Void) {

        let postData = JsonUtil.encodeJsonStringFromObject(requestData)

        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/auth/api/email/password/add")!,timeoutInterval: Double.infinity)
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")
        request.addValue("Bearer \(self.token ?? "")", forHTTPHeaderField: "Authorization")

        request.httpMethod = HTTPMethods.PUT.rawValue
        request.httpBody = postData

        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                print(String(describing: error))
                return
            }

            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<Bool> = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpMessage<Bool>.self) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: Bool = safeImsMessage.data {
                            successAddPasswordAction(userInfoDataSafe);
                        }
                    }
                    //TODO: Add validation action
                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpError.self) {
                        // TODO: Implement proper error handling
                        failureAddPasswordAction(safeError);
                    }
                }
            }
        }

        task.resume()
    }
}

// MARK: - User Http Methods

extension AppState {

    public func getLoggedInUserHttpRequest (successGetUserAction: @escaping (UserInfoDto) -> Void,
                                            failureGetUserAction: @escaping (ImsHttpError) -> Void){

        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/auth/api/user")!, timeoutInterval: Double.infinity)
        request.addValue("Bearer \(self.token ?? "")", forHTTPHeaderField: "Authorization")

        request.httpMethod = HTTPMethods.GET.rawValue

        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                print(String(describing: error))
                return
            }

            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<UserInfoDto> = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpMessage<UserInfoDto>.self) {
                        print(safeImsMessage.status) // 204 is ok
                        if let userInfoDataSafe: UserInfoDto = safeImsMessage.data {
                            DispatchQueue.main.async {
                                self.user = userInfoDataSafe
                                self.logged = true
                                self.token = userInfoDataSafe.token
                            }
                            successGetUserAction(userInfoDataSafe);
                        }
                    }
                    //TODO: Add validation action
                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpError.self) {
                        // TODO: Implement proper error handling
                        failureGetUserAction(safeError);
                    }
                }
            }

        }

        task.resume()
    }

    public func updateUserEmailHttpRequest (requestData: UpdateEmailDto,
                                            successEmailUpdateAction: @escaping (UserInfoDto) -> Void,
                                            failureEmailUpdateAction: @escaping (ImsHttpError) -> Void) {

        let postData = JsonUtil.encodeJsonStringFromObject(requestData);

        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/auth/api/user/email")!,timeoutInterval: Double.infinity)
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")
        request.addValue("Bearer \(self.token ?? "")", forHTTPHeaderField: "Authorization")

        request.httpMethod = HTTPMethods.PUT.rawValue
        request.httpBody = postData

        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                print(String(describing: error))
                return
            }

            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<UserInfoDto> = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpMessage<UserInfoDto>.self) {
                        print(safeImsMessage.status) // 204 is ok
                        if let userInfoDataSafe: UserInfoDto = safeImsMessage.data {
                            DispatchQueue.main.async {
                                self.user = userInfoDataSafe
                                self.logged = true
                                self.token = userInfoDataSafe.token
                            }
                            successEmailUpdateAction(userInfoDataSafe);
                        }
                    }
                    //TODO: Add validation action
                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpError.self) {
                        // TODO: Implement proper error handling
                        failureEmailUpdateAction(safeError);
                    }
                }
            }
        }

        task.resume()
    }

    public func updateUserNicknameHttpRequest(requestData: UpdateNicknameDto,
                                              successNicknameUpdateAction: @escaping (UserInfoDto) -> Void,
                                              failureNicknameUpdateAction: @escaping (ImsHttpError) -> Void) {

        let postData = JsonUtil.encodeJsonStringFromObject(requestData)

        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/auth/api/user/nickname")!,timeoutInterval: Double.infinity)
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")
        request.addValue("Bearer \(self.token ?? "")", forHTTPHeaderField: "Authorization")

        request.httpMethod = HTTPMethods.PUT.rawValue
        request.httpBody = postData

        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                print(String(describing: error))
                return
            }

            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<UserInfoDto> = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpMessage<UserInfoDto>.self) {
                        print(safeImsMessage.status) // 204 is ok
                        if let userInfoDataSafe: UserInfoDto = safeImsMessage.data {
                            DispatchQueue.main.async {
                                self.user = userInfoDataSafe
                                self.logged = true
                                self.token = userInfoDataSafe.token
                            }
                            successNicknameUpdateAction(userInfoDataSafe);
                        }
                    }
                    //TODO: Add validation action
                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpError.self) {
                        // TODO: Implement proper error handling
                        failureNicknameUpdateAction(safeError);
                    }
                }
            }
        }

        task.resume()
    }
}

// MARK: - Facebook Auth Methods

extension AppState {

    public func loginWithFacebookHttpRequest(requestData: AuthenticateWithFacebookProviderDto,
                                             successRegisterWithFacebook: @escaping (UserInfoDto) -> Void,
                                             failureRegisterWithFacebook: @escaping (ImsHttpError) -> Void) {

        let postData = JsonUtil.encodeJsonStringFromObject(requestData)

        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/auth/api/facebook/login")!,timeoutInterval: Double.infinity)
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")

        request.httpMethod = HTTPMethods.POST.rawValue
        request.httpBody = postData

        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                print(String(describing: error))
                return
            }

            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<UserInfoDto> = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpMessage<UserInfoDto>.self) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: UserInfoDto = safeImsMessage.data {
                            DispatchQueue.main.async {
                                self.user = userInfoDataSafe
                                self.logged = true
                                self.token = userInfoDataSafe.token
                            }
                            successRegisterWithFacebook(userInfoDataSafe);
                        }
                    }
                    //TODO: Add validation action
                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpError.self) {
                        // TODO: Implement proper error handling
                        failureRegisterWithFacebook(safeError);
                    }
                }
            }
        }

        task.resume()
    }

    public func addFacebookProviderHttpRequest(requestData: AuthenticateWithFacebookProviderDto,
                                               successAddFacebookProvider: @escaping (Bool) -> Void,
                                               failureAddFacebookProvider: @escaping (ImsHttpError) -> Void){

        let postData = JsonUtil.encodeJsonStringFromObject(requestData)

        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/auth/api/facebook/add")!,timeoutInterval: Double.infinity)
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")
        request.addValue("Bearer \(self.token ?? "")", forHTTPHeaderField: "Authorization")

        request.httpMethod = HTTPMethods.POST.rawValue
        request.httpBody = postData

        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                print(String(describing: error))
                return
            }

            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<Bool> = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpMessage<Bool>.self) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: Bool = safeImsMessage.data {
                            successAddFacebookProvider(userInfoDataSafe);
                        }
                    }
                    //TODO: Add validation action
                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpError.self) {
                        // TODO: Implement proper error handling
                        failureAddFacebookProvider(safeError);
                    }
                }
            }
        }

        task.resume()
    }
}

// MARK: - Google Auth Methods

extension AppState {

    public func loginWithGoogleHttpRequest(requestData: AuthenticateWithGoogleProviderDto,
                                           successRegisterWithGoogle: @escaping (UserInfoDto) -> Void,
                                           failureRegisterWithGoogle: @escaping (ImsHttpError) -> Void) {

        let postData = JsonUtil.encodeJsonStringFromObject(requestData)

        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/auth/api/google/login")!,timeoutInterval: Double.infinity)
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")

        request.httpMethod = HTTPMethods.POST.rawValue
        request.httpBody = postData

        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                print(String(describing: error))
                return
            }

            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<UserInfoDto> = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpMessage<UserInfoDto>.self) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: UserInfoDto = safeImsMessage.data {
                            DispatchQueue.main.async {
                                self.user = userInfoDataSafe
                                self.logged = true
                                self.token = userInfoDataSafe.token
                            }
                            successRegisterWithGoogle(userInfoDataSafe);
                        }
                    }
                    //TODO: Add validation action
                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpError.self) {
                        // TODO: Implement proper error handling
                        failureRegisterWithGoogle(safeError);
                    }
                }
            }
        }

        task.resume()
    }

    public func addGoogleProviderHttpRequest(requestData: AuthenticateWithFacebookProviderDto,
                                               successAddFacebookProvider: @escaping (Bool) -> Void,
                                               failureAddFacebookProvider: @escaping (ImsHttpError) -> Void){

        let postData = JsonUtil.encodeJsonStringFromObject(requestData)

        var request = URLRequest(url: URL(string: self.httpBaseUrl + "/auth/api/facebook/add")!,timeoutInterval: Double.infinity)
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")
        request.addValue("Bearer \(self.token ?? "")", forHTTPHeaderField: "Authorization")

        request.httpMethod = HTTPMethods.POST.rawValue
        request.httpBody = postData

        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data else {
                print(String(describing: error))
                return
            }

            if let httpResponse = response as? HTTPURLResponse {
                if(httpResponse.statusCode == 200)
                {
                    if let safeImsMessage: ImsHttpMessage<Bool> = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpMessage<Bool>.self) {
                        print(safeImsMessage.status)
                        if let userInfoDataSafe: Bool = safeImsMessage.data {
                            successAddFacebookProvider(userInfoDataSafe);
                        }
                    }
                    //TODO: Add validation action
                } else {
                    if let safeError: ImsHttpError = JsonUtil.decodeJsonData(data: data, returnModelType: ImsHttpError.self) {
                        // TODO: Implement proper error handling
                        failureAddFacebookProvider(safeError);
                    }
                }
            }
        }

        task.resume()
    }
}