package com.inmotion.inmotionserverjava.services.interfaces;

import org.springframework.http.MediaType;

public interface MinioService {
    void uploadFile(String bucketName, String filePath, byte[] file, MediaType mediaType);
    byte[] getFile(String bucketName, String filePath);
}
