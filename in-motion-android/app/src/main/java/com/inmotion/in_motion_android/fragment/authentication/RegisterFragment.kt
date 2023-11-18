package com.inmotion.in_motion_android.fragment.authentication

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.navigation.NavController
import androidx.navigation.fragment.findNavController
import com.inmotion.in_motion_android.InMotionApp
import com.inmotion.in_motion_android.R
import com.inmotion.in_motion_android.data.dto.auth.RegisterUserWithEmailAndPasswordDto
import com.inmotion.in_motion_android.data.dto.auth.SuccessfullRegistrationResponseDto
import com.inmotion.in_motion_android.data.repository.AuthenticationRepository
import com.inmotion.in_motion_android.data.repository.RepositoryCallback
import com.inmotion.in_motion_android.databinding.FragmentRegisterBinding

class RegisterFragment : Fragment() {

    private lateinit var binding: FragmentRegisterBinding
    private lateinit var navController: NavController
    private lateinit var authenticationRepository: AuthenticationRepository
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = FragmentRegisterBinding.inflate(layoutInflater)
        navController = this.findNavController()
        authenticationRepository =
            AuthenticationRepository((activity?.application as InMotionApp).db.userInfoDao())
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        binding.tvLogin.setOnClickListener {
            navController.navigate(R.id.action_registerFragment_to_loginFragment2)
        }

        binding.btnRegister.setOnClickListener {
            val nickname = binding.etNickname.text.toString()
            val email = binding.etEmail.text.toString()
            val password = binding.etPassword.text.toString()
            val repeatPassword = binding.etRepeatPassword.text.toString()
            val registerUserWithEmailAndPasswordDto =
                RegisterUserWithEmailAndPasswordDto(email, password, repeatPassword, nickname)

            authenticationRepository.registerWithEmail(
                registerUserWithEmailAndPasswordDto,
                object : RepositoryCallback<SuccessfullRegistrationResponseDto> {
                    override fun onResponse(response: SuccessfullRegistrationResponseDto) {
                        Toast.makeText(
                            activity,
                            "Check your email ${response.email} to finish registration!",
                            Toast.LENGTH_LONG
                        )
                            .show()
                        navController.navigate(R.id.action_registerFragment_to_loginFragment2)
                    }

                    override fun onFailure() {
                        Toast.makeText(activity, "Invalid data provided", Toast.LENGTH_SHORT).show()
                    }
                }
            )
        }
    }
}