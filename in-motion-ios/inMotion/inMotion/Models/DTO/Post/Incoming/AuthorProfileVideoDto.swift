//
//  AuthorProfileVideoDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct AuthorProfileVideoDto: Codable {
    public var id: UUID
    public var filename: String
    public var bucketName: String
    public var bucketLocation: String
    public var contentType: String
}
