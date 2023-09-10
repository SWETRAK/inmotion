package com.inmotion.inmotionserverjava;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.ws.config.annotation.EnableWs;


@EnableWs
@SpringBootApplication
public class InMotionServerJavaApplication {

    public static void main(String[] args) {
        SpringApplication.run(InMotionServerJavaApplication.class, args);
    }
}
