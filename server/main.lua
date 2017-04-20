--[[players = {}

RegisterServerEvent("freeroam:newplayer")
AddEventHandler("freeroam:newplayer", function()
	table.insert(players, source)
end)]]