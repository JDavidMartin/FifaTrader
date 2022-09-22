Essentially an interface for interacting with FUT API, specialising in mass buying and selling of a single player at a time.

## Running The App ##
Before you can run this App you'll need to download and install at least .Net Core 3.1 sdk
https://dotnet.microsoft.com/download/dotnet-core/3.1
Then simply run using your preferred IDE (I developed this using Visual Studio Community 2019).
https://visualstudio.microsoft.com/vs/

## Setting Up ##
When you load the App, you'll be greeted by the home page with a token banner across the top, asking for your X-UT-SID token
This is the access token generated when you login to the Fifa Web App.

The easiest way to get this token is to download and install the chrome extension Web Sniffer
https://chrome.google.com/webstore/detail/live-http-headers/ianhploojoffmpcpilhgpacbeaifanid?hl=en
When you run this extension, it records all outgoing calls made by your browser, including any headers and parameters of these requests.

With this extension running, go on the Fifa Web App, and go to your Squad page.
The Web Sniffer extension should have picked up a request made to "/ut/game/fifa20/featuredsquad/fullhistory". One of the request headers in the request will be your X-UT-SID token. Copy this into the token banner back on the App.

To verify your token is valid, press the Validate Token button.
If your token is valid a small green circle should appear.
If your token is invalid or expired, a red circle will appear instead.

And that's you set up to go!
Eventually your token will expire, when this happens simply repeat the above steps again.

## Buying Players ##
On the nav bar on the left hand side, click on the Buy Players tab.

There will be 2 form boxes on this page. One for the price you want to bid, another for the player Id.

To find the player Id, run Web Sniffer again, and search for your desired player on the Transfer Market.
The extension should have picked up a request to "/ut/game/fifa20/transfermarket". This request will have several parameters, including one called "maskedDefId" with a number inside.

Copy this number as the ID of the player you are bidding on.
Set the price you want to bid. 
And press the Bid button.

The view at the bottom should begin populating with players being bid on.

If the bid is successful, the status will turn green.
If the bid unsuccessful, the status will turn red, and all future bids will fail.
This is done to prevent calls being made when you have a temporary market ban, and make things worse by continuing to try and bid.

## Managing Players ##
On the nav var on the left hand side, click on the Transfer Targets tab.

Click on the "Refresh Players" button.

This will update the view with a summary of all players in your Transfer Targets.
Won players have a solid green status circle.
Winning players have a green spinner status.
Lost players have a solid red status circle.
Losing players have a red spinner status.

"Clear Expired" players will remove all Lost or Losing players

"Sell Players" will sell all players at the price set in the two forms at the top of the page.
One form is Starting Bid, the other is Buy Now Price

## Future Plans ##
Adding in Transfer List functionality, to manage players currently selling.

Adding in Unassigned functionality, to allow users to move players to unsassigned, and then move to transfer list.

Create a method to store favourited player ID's, to be easily accessed on the Bidding page. - DONE
