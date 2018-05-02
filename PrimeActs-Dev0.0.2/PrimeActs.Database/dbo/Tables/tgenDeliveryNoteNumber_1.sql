CREATE TABLE [dbo].[tgenDeliveryNoteNumber] (
    [DeliveryNoteID]     UNIQUEIDENTIFIER NOT NULL,
    [DeliveryNoteNumber] INT              IDENTITY (1, 1) NOT NULL, 
    CONSTRAINT [PK_tgenDeliveryNoteNumber] PRIMARY KEY ([DeliveryNoteID])
);

