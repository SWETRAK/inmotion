package com.inmotion.inmotionserverjava.exceptions.converter;

public class BadFileExtensionException extends RuntimeException{
    public BadFileExtensionException() {
        super("File extension is not mp4!");
    }
}
