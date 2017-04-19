local missionClient

RegisterServerEvent("freeroam:missionrunning")
AddEventHandler("freeroam:missionrunning", function(source, client, state)
	if not state then
		missionClient = nil
	else
		missionClient = source
	end

	TriggerClientEvent("freeroam:missionrunning", -1, client, state)
end)

AddEventHandler("playerDropped", function(reason)
	if source == missionClient then
		missionClient = nil
		TriggerClientEvent("freeroam:missionrunning", -1, false)
	end
end)