local missionClient

RegisterServerEvent("freeroam:newplayer")
AddEventHandler("freeroam:newplayer", function(source)
	if missionClient then
		TriggerClientEvent("freeroam:missionrunning", source, GetPlayerName(missionClient), true)
	end
end)

RegisterServerEvent("freeroam:missionrunning")
AddEventHandler("freeroam:missionrunning", function(source, clientHandle, state)
	if not state then
		missionClient = nil
	else
		missionClient = source
	end

	TriggerClientEvent("freeroam:missionrunning", -1, clientHandle, state)
end)

AddEventHandler("playerDropped", function(reason)
	if source == missionClient then
		missionClient = nil
		TriggerClientEvent("freeroam:missionrunning", -1, GetPlayerName(source), false)
	end
end)