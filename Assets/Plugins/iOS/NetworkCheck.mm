//
//  NetworkCheck.mm
//
//  Created by Andre Pothier on 2019-02-12.
//  Copyright © 2019 Andre Pothier. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <SystemConfiguration/SystemConfiguration.h>
@interface NetworkCheck : NSObject
{
    SCNetworkReachabilityFlags *flags;
}
@end


@implementation NetworkCheck

static NetworkCheck *sharedInstance;
+(NetworkCheck*) sharedInstance
{
    static dispatch_once_t onceToken;
    dispatch_once( &onceToken, ^{
        NSLog(@"Creating Shared Instance");
        sharedInstance = [[NetworkCheck alloc] init];
    });
    return sharedInstance;
}

-(id) init
{
    self = [super init];
    if(self)
        [self initHelper];
    
    return self;
}

-(void) initHelper
{
    NSLog(@"Initilized");
}

-(void) Start
{
    //DO START UP CODE HERE IF NEEDED
    NSLog(@"Started NetworkCheck");
}

-(bool)CheckNetwork
{
    //set to check Googles Domain
    SCNetworkReachabilityRef reachability = SCNetworkReachabilityCreateWithName(NULL, "8.8.8.8");
    
    SCNetworkReachabilityFlags flag;
    
    //Checks if There is a Network, and sets flag
    bool success = SCNetworkReachabilityGetFlags(reachability, &flag);
    
    flags = &flag;
    
    //Releases the Reachability resource
    CFRelease(reachability);
    
    if(success)
    {
        bool isReachable = ((*flags & kSCNetworkReachabilityFlagsReachable) != 0);
        bool needsConnection = ((*flags & kSCNetworkReachabilityFlagsConnectionRequired) != 0);
        bool isNetworkReachable = (isReachable && !needsConnection);
        
        if (!isNetworkReachable) {
            return false;
        }
        return true;
    }
    return false;
}

-(bool)CheckMobile
{
    bool online = [self CheckNetwork];
    
    if(online){
        if ((*flags & kSCNetworkReachabilityFlagsIsWWAN) != 0) {
            return true;
        }
    }
    return false;
}

-(bool)CheckWiFi
{
    bool online = [self CheckNetwork];
    
    if(online){
        if ((*flags & kSCNetworkReachabilityFlagsIsWWAN) == 0) {
            return true;
        }
    }
    return false;
}

@end

extern "C" {
    
    void InitilizeNetwork(){
        [[NetworkCheck sharedInstance] Start];
    }
    
    bool GetNetworkStatus(){
        return ( [[NetworkCheck sharedInstance] CheckNetwork]);
    }
    
    bool GetMobileStatus(){
        return ( [[NetworkCheck sharedInstance] CheckMobile]);
    }
    
    bool GetWiFiStatus(){
        return ( [[NetworkCheck sharedInstance] CheckWiFi]);
    }
    
}
