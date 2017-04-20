RegisterServerEvent("freeroam:playerjoined")
AddEventHandler("freeroam:playerjoined", function(source)
	TriggerClientEvent("freeroam:playerjoined", -1, GetPlayerName(source))
end)

AddEventHandler("playerDropped", function(reason)
	TriggerClientEvent("freeroam:playerleft", -1, GetPlayerName(source))
end)