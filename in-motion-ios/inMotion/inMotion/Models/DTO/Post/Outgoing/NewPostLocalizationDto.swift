//
//  NewPostLocalizationDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct NewPostLocalizationDto: Codable {
    public var name: String
    public var latitude: Double
    public var longitude: Double
}
