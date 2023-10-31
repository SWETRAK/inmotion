//
//  RequestFriendshipDto.swift
//  inMotion
//
//  Created by Kamil Pietrak on 31/10/2023.
//

import Foundation

struct RequestFriendshipDto: Codable {
    public var id: UUID
    public var externalUserId: UUID
    public var externalUser: FriendInfoDto
    public var requested: Date
}
